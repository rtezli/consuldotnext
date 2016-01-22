module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);
  var constants = {
    'binDir': './bin',
    'tmpDir': './tmp',
    'appDir': './Pixills.Consul.Client',
    'testsDir': './Pixills.Consul.Client.Tests',
    'release': 'Release',
    'debug': 'Debug',
    'framework': 'dotnet5.4'
  };
  grunt.initConfig({
    const: constants,
    watch: {
      files: ['<%= const.appDir %>/**/*.*'],
      tasks: ['build']
    },
    run: {
      restore_project: {
        cmd: '<%= const.appDir %>',
        exec: 'dnu restore'
      },
      restore_tests: {
        cmd: '<%= const.testsDir %>',
        exec: 'dnu restore'
      },
      build_app: {
        cmd: '<%= const.appDir %>',
        exec: 'dnu build ./Pixills.Consul.Client/project.json --configuration <%= const.release %> --out <%= const.tmpDir %>'
      },
      build_tests: {
        cmd: '<%= const.testsDir %>',
        exec: 'dnu build <%= const.testsDir %>/project.json --configuration <%= const.release %> --out <%= const.tmpDir %>'
      },
      test: {
        cmd: '<%= const.testsDir %>',
        exec: 'dnx -p <%= const.appDir %> test'
      },
      pack: {
        cmd: '<%= const.appDir %>',
        exec: 'dnu pack <%= const.appDir %>/project.json --framework <%= const.framework %> --configuration <%= const.release %> --out <%= const.tmpDir %>'
      }
    },
    copy: {
      binaries: {
        files: [{
          expand: true,
          src: ['<%= const.tmpDir %>/**/*.dll', '!<%= const.tmpDir %>/**/*.Tests.dll', '<%= const.tmpDir %>/**/*.nupkg', '!<%= const.tmpDir %>/**/*.symbols.nupkg'],
          dest: '<%= const.binDir %>',
          flatten: true
        }]
      }
    },
    clean: {
      bin: {
        options: {
          force: true
        },
        src: ['<%= const.binDir %>']
      },
      temp: {
        options: {
          force: true
        },
        src: ['<%= const.tmpDir %>']
      }
    }
  });

  grunt.registerTask('restore', ['run:restore_project', 'run:restore_tests']);
  grunt.registerTask('build',   ['clean:bin', 'run:build_app', 'run:build_tests', 'run:pack', 'copy', 'clean:temp']);
  grunt.registerTask('test',    ['run:test']);
  grunt.registerTask('dev',     ['watch']);
  grunt.registerTask('default', ['build']);
};
