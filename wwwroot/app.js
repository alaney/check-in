var schoolsRequest = new Request("./api/students/schools")
var app
var welcome = "Welcome to Greg Machen Photography"
var chooseSchool = "Choose your school"
var findYourName = "Find and select your name"
var info = "Please enter your phone number and at least 2 email addresses"
window.onload = function() {
  app = new Vue({
    el: "#app",
    data: {
      students: [],
      schools: [],
      step: 0,
      search: "",
      student: null,
      primary: welcome,
      secondary: chooseSchool
    },
    methods: {
      onSchoolClick: function(name) {
        getStudents(name)
      },
      onStudentClick: function(student) {
        getStudent(student.clientId)
      },
      onStudentSave: function() {
        patchStudent(this.student)
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
      app.schools = []
      r.forEach(s => {
        app.schools.push(s)
      })
    })
}

function getStudents(schoolName) {
  app.primary = schoolName
  app.secondary = findYourName
  var studentsRequest = new Request(`./api/students?schoolName=${schoolName}`)
  fetch(studentsRequest)
    .then(r => {
      return r.json()
    })
    .then(r => {
      app.students = []
      r.forEach(s => {
        app.students.push(s)
      })
      app.step += 1
    })
}

function getStudent(id) {
  app.secondary = info
  var studentsRequest = new Request(`./api/students/${id}`)
  fetch(studentsRequest)
    .then(r => {
      return r.json()
    })
    .then(r => {
      app.student = null
      app.student = r
      app.step += 1
    })
}

function patchStudent(student) {
  var headers = new Headers()
  headers.append("Content-Type", "application/json")
  var studentsRequest = new Request(`./api/students/${student.clientId}`, {
    method: "PATCH",
    body: JSON.stringify(student),
    headers
  })
  fetch(studentsRequest).then(r => {
    app.step += 1
    setTimeout(function() {
      app.primary = welcome
      app.secondary = chooseSchool
      app.step = 0
    }, 3000)
  })
}

Vue.component("school", {
  props: ["name"],
  template: '<div class="tile box is-child notification is-primary"><p class="title">{{name}}</p></div>'
})
