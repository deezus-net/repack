module.exports = {
    mode: 'development',
    entry: {
        'stack-edit': './ts/stack-edit.ts',
        'task-edit': './ts/task-edit.ts'
    },
    output: {
        filename: '[name].js',
        path: __dirname + '/wwwroot/js'
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: 'ts-loader'
            }
        ]
    },
    resolve: {
        extensions: [
            '.ts','.js'
        ]
    }
};