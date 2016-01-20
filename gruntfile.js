module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);

  grunt.initConfig({
    watch: {
      files: ['src/**/*.*'],
      tasks: ['build']
    },
    run: {
     restore: {
          exec: 'cd src && dnu restore',
     },
     build: {
         exec: 'dnu build ./src/project.json --out ./bin',
     }
   }
  });

  grunt.registerTask('restore', ['run:build']);
  grunt.registerTask('build', ['run:build']);
  grunt.registerTask('default', ['watch']);

};
