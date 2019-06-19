var schoolsRequest = new Request("./api/students/schools")
var app
var chooseSchool = "Choose your school"
var findYourName = "Find and select your name"
var info = "Please fill out the information below"

function doNothing() { }

window.onload = function () {
  app = new Vue({
    el: "#app",
    data: {
      loading: true,
      showEmailWarning: false,
      showPhoneWarning: false,
      students: [],
      schools: [],
      step: 0,
      search: "",
      student: null,
      primary: "",
      secondary: chooseSchool,
      schoolSettings: []
    },
    methods: {
      onSchoolClick: function (name) {
        getStudents(name)
      },
      onStudentClick: function (student) {
        getStudent(student.clientId)
      },
      onStudentSave: function () {
        patchStudent(this.student)
      },
      startOver: function () {
        this.students = []
        this.step = 0
        this.search = ""
        this.student = null
        this.primary = ""
        this.secondary = chooseSchool
      }
    },
    computed: {
      filteredStudents() {
        return this.students.filter(s => {
          return s.firstName.toLowerCase().indexOf(this.search.toLowerCase()) > -1
        })
      }
    }
  })

  fetch(schoolsRequest)
    .then(r => {
      return r.json()
    })
    .then(r => {
      app.loading = false
      app.schools = []
      r.forEach(s => {
        app.schools.push(s)
      })
    })
}

function getStudents(schoolName) {
  app.loading = true
  var studentsRequest = new Request(`./api/students?schoolName=${schoolName}`)
  fetch(studentsRequest)
    .then(r => {
      return r.json()
    })
    .then(r => {
      app.loading = false
      app.primary = schoolName
      app.secondary = findYourName
      app.students = []
      r.forEach(s => {
        app.students.push(s)
      })
      app.step += 1
    })
}

function getStudent(id) {
  app.loading = true
  var studentsRequest = new Request(`./api/students/${id}`)
  fetch(studentsRequest)
    .then(r => {
      return r.json()
    })
    .then(r => {
      app.loading = false
      app.secondary = info
      app.primary = `${r.firstName} ${r.lastName}`
      app.student = null
      app.student = r
      app.step += 1
    })
    .then(function () {
      new Cleave("#phone1", {
        phone: true,
        phoneRegionCode: "us"
      })

      new Cleave("#phone2", {
        phone: true,
        phoneRegionCode: "us"
      })
    })
}

function patchStudent(student) {

  // input validation
  var inputs = document.getElementsByTagName("input")
  var anyInvalid = false
  for (let i = 0; i < inputs.length; i++) {
    const element = inputs[i]
    if (element.validationMessage) {
      element.classList.add("is-danger")
      anyInvalid = true
    } else {
      element.classList.remove("is-danger")
    }
  }

  if (anyInvalid) {
    return
  }

  // check for at least two emails.. I don't think this will be used...
  if (app.student.emails.filter(e => !!e).length < 2) {
    app.showEmailWarning = true
    setTimeout(function () {
      app.showEmailWarning = false
    }, 3000)
    return
  }

  // or this :(
  if (!app.student.phone1) {
    app.showPhoneWarning = true
    setTimeout(function () {
      app.showPhoneWarning = false
    }, 3000)
    return
  }

  var headers = new Headers()
  headers.append("Content-Type", "application/json")
  var studentsRequest = new Request(`./api/students/${student.clientId}`, {
    method: "PATCH",
    body: JSON.stringify(student),
    headers
  })
  app.loading = true

  fetch(studentsRequest)
    .then(r => {
      app.loading = false
      app.step += 1
      app.primary = "Thank You!"
      app.secondary = ""
      setTimeout(function () {
        app.students = []
        app.step = 0
        app.search = ""
        app.student = null
        app.primary = ""
        app.secondary = chooseSchool
      }, 6000)
    })
    .catch(e => {
      console.error(e)
    })
}

Vue.component("welcome", {
  template:
    '<h1 class="title">Welcome to <span style="font-family: Monotype Corsiva, Italianno, cursive; font-size: 2.5rem;" >Greg Machen Photography</span></h1>'
})

Vue.component("school", {
  props: ["name"],
  template: '<div class="tile box is-child notification is-primary"><p class="title">{{name}}</p></div>'
})
