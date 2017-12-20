/// <binding ProjectOpened='watch' />
module.exports = function (grunt) {
    require('jit-grunt')(grunt);

    grunt.initConfig({
        less: {
            development: {
                options: {
                    compress: false,
                    yuicompress: true,
                    optimization: 2
                },
                files: {
                    "Content/Site.css": "App/app.less" // destination file and source file
                }
            }
        },
        watch: {
            styles: {
                files: ['App/**/*.less'], // which files to watch
                tasks: ['less'],
                options: {
                    nospawn: true
                }
            }
        }
    });

    grunt.registerTask('default', ['less']);
    grunt.task.loadNpmTasks("grunt-contrib-watch");
};