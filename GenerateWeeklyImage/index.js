const phantom = require('phantom');

module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    const phantom = require('phantom');

    if ((req.query.year || (req.body && req.body.year)) && (req.query.weekNumber || (req.body && req.body.weekNumber))) {
        const instance = await phantom.create();
        const page = await instance.createPage();

        const status = await page.open('https://red-folder.com');

        context.res = {
            // status: 200, /* Defaults to 200 */
            body: "Hello " + status;
        };
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass Year (year) and Week Number (weekNumber) on the query string or in the request body"
        };
    }
    context.done();
};