var schoolsRequest = new Request("./api/students/schools");
var app;
window.onload = function () {
  
  app = new Vue({
    el: '#app',
    data: {
      students: [],
      schools: [],
      step: 0,
      search: "",
      student: null
    },
    methods: {
      onSchoolClick: function (name) {
        getStudents(name);
      },
      onStudentClick: function(student) {
        getStudent(student.clientId)
      }
    },
    computed: {
      filteredStudents() {
        return this.students.filter(s => {
           return s.firstName.toLowerCase().indexOf(this.search.toLowerCase()) > -1
        })
      }
    }
  });
  
  fetch(schoolsRequest).then(r => {
    return r.json();
  }).then(r => {
    app.schools = [];
    r.forEach(s => {
      app.schools.push(s);
    });
  });
}

function getStudents(schoolName) {
  var studentsRequest = new Request(`./api/students?schoolName=${schoolName}`);
  fetch(studentsRequest).then(r => {
    return r.json();
  }).then(r => {
    app.students = [];
    r.forEach(s => {
      app.students.push(s);
    });
    app.step += 1;
  });
}

function getStudent(id) {
  var studentsRequest = new Request(`./api/students/${id}`);
  fetch(studentsRequest).then(r => {
    return r.json();
  }).then(r => {
    app.student = r;
    app.step += 1;
  });
}

Vue.component('school', {
  props: ['name'],
  template: '<div class="tile box is-child notification is-primary"><p class="title">{{name}}</p></div>'
})