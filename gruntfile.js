module.exports = function(grunt) {

  require('load-grunt-tasks')(grunt);

  var constants = {
    'binDir': './bin',
    'tmpDir': './tmp',
    'appDir': './Pixills.Consul.Client',
    'testsDir': './Pixills.Consul.Client.Tests',
    'release': 'Release',
    'debug': 'Debug',
    'framework': 'dnxcore50',
    'config' : 'Debug'
  };

  grunt.initConfig({
    const: constants,
    watch: {
      files: ['<%= const.appDir %>/**/*.*'],
      tasks: ['build']
    },
    shell: {
      export_debug: {
        command: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Debug'
      },
      export_release: {
        command: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Release'
      },
      restore: {
        command: 'dnu restore'
      },
      build_app: {
        options: {
          execOptions: {
            cwd: '<%= const.appDir %>'
          }
        },
        command: 'dnu build --configuration <%= const.config %>'
      },
      build_tests: {
        options: {
          execOptions: {
            cwd: '<%= const.testsDir %>'
          }
        },
        command: 'dnu build --configuration <%= const.config %>'
      },
      test: {
        options: {
          execOptions: {
            cwd: '<%= const.testsDir %>'
          }
        },
        command: 'dnx test'
      },
      pack: {
        options: {
          execOptions: {
            cwd: '<%= const.appDir %>'
          }
        },
        command: 'dnu pack --framework <%= const.framework %> --configuration <%= const.config %>'
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

  grunt.registerTask('restore', ['shell:restore']);
  grunt.registerTask('build-post', ['clean:bin', 'shell:build_app', 'shell:build_tests', 'shell:pack', 'copy', 'clean:temp']);
  grunt.registerTask('build-release', ['env:release', 'build-post']);
  grunt.registerTask('build-debug', ['env:debug', 'build-post']);
  grunt.registerTask('build', ['build-release']);
  grunt.registerTask('test', ['shell:test']);
  grunt.registerTask('dev', ['watch']);
  grunt.registerTask('default', ['build']);
};
