const ChartjsNode = require('chartjs-node');

module.exports = function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    if (req.query.name || (req.body && req.body.name)) {
        
        const chartJsOptions = {
    type: 'bar',
    data: {
        labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }]
    }};
    
    /*
    ,
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero:true
                }
            }]
        }
    }
        };
        */
        
        // 600x600 canvas size
        var chartNode = new ChartjsNode(600, 600);
        return chartNode.drawChart(chartJsOptions)
            .then(() => {
                // chart is created
 
                // get image as png buffer
                return chartNode.getImageBuffer('image/png');
            })
            .then(buffer => {
                Array.isArray(buffer) // => true
                // as a stream
                return chartNode.getImageStream('image/png');
            })
            .then(streamResult => {
                // using the length property you can do things like
                // directly upload the image to s3 by using the
                // stream and length properties
                streamResult.stream // => Stream object
                streamResult.length // => Integer length of stream
                // write to a file
                return chartNode.writeImageToFile('image/png', './testimage.png');
            })
            .then(() => {
                // chart is now written to the file path
                // ./testimage.png
                context.res = {
                    // status: 200, /* Defaults to 200 */
                    body: "Hello " + (req.query.name || req.body.name)
                };
            });
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass a name on the query string or in the request body"
        };
    }
    context.done();
};