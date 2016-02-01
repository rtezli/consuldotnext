module.exports = function(grunt) {

    require('load-grunt-tasks')(grunt);

    process.env.CONSUL_CLIENT_NODE_NAME = "test-node";
    process.env.CONSUL_CLIENT_SERVICE_NAME = "test-service";
    process.env.CONSUL_CLIENT_DATACENTER_NAME = "test-datacenter";
    process.env.CONSUL_AGENT_HOSTNAME = "localhost";
    process.env.CONSUL_AGENT_PORT = "8500";

    var constants = {
        'rootDir': './',
        'binDir': './bin',
        'tmpDir': './tmp',
        'appDir': './Pixills.Consul.Client',
        'unitTestsDir': './Pixills.Consul.Client.UnitTests',
        'integrationTestsDir': './Pixills.Consul.Client.IntegrationTests',
        'release': 'Release',
        'debug': 'Debug',
        'framework': 'dnxcore50',
        'config': 'Debug'
    };

    grunt.initConfig({
        const: constants,
        watch: {
            files: ['<%= const.appDir %>/**/*.cs', '<%= const.unitTestsDir %>/**/*.cs'],
            tasks: ['build']
        },
        run: {
            consul: {
                options: {
                    quiet: true,
                    wait: false
                },
                exec: 'consul agent -server -bootstrap -data-dir ./consuldata -log-level=debug -http-port=' + process.env.CONSUL_AGENT_PORT + ' -dc=' + (process.env.CONSUL_CLIENT_DATACENTER_NAME || 'dc1')
            },
            export_debug: {
                exec: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Debug'
            },
            export_release: {
                exec: 'export PIXILLS_CONSUL_CLIENT_BUILD_CONFIG=Release'
            },
            restore: {
                exec: 'dnu restore --quiet'
            },
            build_app: {
                options: {
                    cwd: '<%= const.appDir %>'
                },
                exec: 'dnu build --configuration <%= const.config %> --quiet'
            },
            build_unit_tests: {
                options: {
                    cwd: '<%= const.unitTestsDir %>'
                },
                exec: 'dnu build --configuration <%= const.config %> --quiet'
            },
            build_int_tests: {
                options: {
                    cwd: '<%= const.integrationTestsDir %>'
                },
                exec: 'dnu build --configuration <%= const.config %> --quiet'
            },
            unittest: {
                options: {
                    cwd: '<%= const.unitTestsDir %>'
                },
                exec: 'dnx test'
            },
            integrationtest: {
                options: {
                    cwd: '<%= const.integrationTestsDir %>'
                },
                exec: 'dnx test'
            },
            pack: {
                options: {
                    cwd: '<%= const.appDir %>'
                },
                exec: 'dnu pack --framework <%= const.framework %> --configuration <%= const.config %> --quiet'
            }
        },
        copy: {
            binaries: {
                files: [{
                    expand: true,
                    src: ['<%= const.tmpDir %>/**/*.dll', '<%= const.tmpDir %>/**/*.pdb', '!<%= const.tmpDir %>/**/*Tests.dll', '<%= const.tmpDir %>/**/*.nupkg', '!<%= const.tmpDir %>/**/*.symbols.nupkg'],
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

    grunt.registerTask('restore', ['run:restore']);
    grunt.registerTask('build', ['clean:bin', 'run:build_app', 'run:build_tests', 'run:pack', 'copy', 'clean:temp']);
    grunt.registerTask('unit-test', ['restore', 'clean:bin', 'run:build_app', 'run:build_tests', 'run:unittest']);
    grunt.registerTask('int-test', ['restore', 'clean:bin', 'run:build_app', 'run:build_unit_tests','run:consul', 'run:integrationtest']);
    grunt.registerTask('ct', ['unittest', 'watch']);
    grunt.registerTask('default', ['build']);
};
