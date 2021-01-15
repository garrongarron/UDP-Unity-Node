const dgram = require('dgram');

const server = dgram.createSocket('udp4');

server.on('error', (err) => {
    console.log(`server error:\n${err.stack}`);
    server.close();
});

let db = {};
server.on('message', (msg, senderInfo) => {
    console.log('Messages received ' + msg)
    let ipPort = senderInfo.address + ":" + senderInfo.port;
    db[ipPort] = ipPort;
    let keys = Object.keys(db);
    for (let index = 0; index < keys.length; index++) {        
        let s = keys[index].split(":");
        server.send(msg, s[1], s[0],()=>{
            console.log(`Message ${msg} sent to ${s[0]}:${s[1]}`)
        })
    }
});

server.on('listening', () => {
    const address = server.address();
    console.log(`server listening on ${address.address}:${address.port}`);
});

server.bind(5500);

