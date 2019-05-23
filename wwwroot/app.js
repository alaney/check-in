var studentsRequest = new Request("./api/students");
var schoolsRequest = new Request("./api/students/schools");
var app;
window.onload = function () {
  
  app = new Vue({
    el: '#app',
    data: {
      students: [],
      schools: []
    }
  });
  
  fetch(studentsRequest).then(r => {
    return r.json();
  }).then(r => {
    r.forEach(s => {
      app.students.push(s);
    });
  });

  fetch(schoolsRequest).then(r => {
    return r.json();
  }).then(r => {
    r.forEach(s => {
      app.schools.push(s);
    });
  });
}

Vue.component('school', {
  props: ['name'],
  template: '<article class="tile is-child notification is-primary"><p class="title">{{name}}</p></article>'
})