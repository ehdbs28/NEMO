const fs = require('fs');
const ws = require('ws');
const wss = new ws.Server({port:3000});

let idcnt = 0;
var file = require('./scoreFile.json');

wss.on('listening', () => {
    console.log(`server open port ${wss.options.port}`);
});

wss.on('connection', socket => {
    socket.id = idcnt++;
    socket.on('message', msg => {
        let data;
        console.log(msg.toString());
        try { data = JSON.parse(msg.toString()); }
        catch(err) { console.log("error"); return; }
        console.log(data);
        if(data.type == "ScoreData")
        {
            var scoreData = {
                point : data.point,
                time : data.time,
                name : data.name
            }
            console.log("Push");
            file.push(scoreData);
            fs.writeFileSync('./scoreFile.json', JSON.stringify(file));
            console.log("Save");
        }
    });
});