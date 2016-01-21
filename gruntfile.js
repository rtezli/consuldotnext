module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);

  grunt.initConfig({
    watch: {
      files: ['consuldotnext/**/*.*'],
      tasks: ['build']
    },
    run: {
     restore: {
          exec: 'cd src && dnu restore',
     },
     build: {
         exec: 'dnu build ./consuldotnext/project.json --out ./bin',
     }
   }
  });

  grunt.registerTask('restore', ['run:build']);
  grunt.registerTask('build', ['run:build']);
  grunt.registerTask('default', ['watch']);

};
