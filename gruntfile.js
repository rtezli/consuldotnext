module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);
  var constants = {
    'binDir': './bin',
    'tmpDir': './tmp',
    'appDir': './Pixills.Consul.Client',
    'testsDir': './Pixills.Consul.Client.Tests',
    'release': 'Release',
    'debug': 'Debug',
    'framework': 'dnxcore50'
  };
  grunt.initConfig({
    const: constants,
    watch: {
      files: ['<%= const.appDir %>/**/*.*'],
      tasks: ['build']
    },
    env: {
      debug: {
        PIXILLS_CONSUL_CLIENT_BUILD_CONFIG: 'Debug',
      },
      release: {
        PIXILLS_CONSUL_CLIENT_BUILD_CONFIG: 'Release',
      }
    },
    run: {
      export_debug: {
        exec: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Debug',
      },
      export_release: {
        exec: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Release',
      },
      restore_project: {
        cwd: '<%= const.appDir %>',
        exec: 'dnu restore'
      },
      restore_tests: {
        cwd: '<%= const.testsDir %>',
        exec: 'dnu restore'
      },
      build_app: {
        cwd: '<%= const.appDir %>',
        exec: 'dnu build ./Pixills.Consul.Client/project.json --configuration $PIXILLS_CONSUL_CLIENT_BUILD_CONFIG'
      },
      build_tests: {
        cwd: '<%= const.testsDir %>',
        exec: 'dnu build <%= const.testsDir %>/project.json --configuration $PIXILLS_CONSUL_CLIENT_BUILD_CONFIG'
      },
      test: {
        cwd: '<%= const.testsDir %>',
        exec: 'dnx test'
      },
      pack: {
        cwd: '<%= const.appDir %>',
        exec: 'dnu pack <%= const.appDir %>/project.json --framework <%= const.framework %> --configuration <%= const.release %> --out <%= const.tmpDir %>'
      }
    },
    copy: {
      binaries: {
        files: [{
          expand: true,
          src: ['<%= const.tmpDir %>/**/*.dll', '<%= const.tmpDir %>/**/*.pdb', '!<%= const.tmpDir %>/**/*.Tests.dll', '<%= const.tmpDir %>/**/*.nupkg', '!<%= const.tmpDir %>/**/*.symbols.nupkg'],
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
  grunt.registerTask('build-post', ['clean:bin', 'run:build_app', 'run:build_tests', 'run:pack', 'copy', 'clean:temp']);
  grunt.registerTask('build-release', ['env:debug', 'build-post']);
  grunt.registerTask('build-debug', ['env:release', 'build-post']);
  grunt.registerTask('build', ['build-release']);
  grunt.registerTask('test', ['run:test']);
  grunt.registerTask('dev', ['watch']);
  grunt.registerTask('default', ['build']);
};
