const dgram = require('dgram');

const client = dgram.createSocket('udp4');

client.send("asdasd", 5500, 'localhost', (err)=>{
    if(err) throw err;

    console.log("UDP message sent");

    client.close();
})