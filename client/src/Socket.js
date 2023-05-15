import * as signalR from "@microsoft/signalr";

class Socket {
    static _instance = null;
    static get instance() {
        if (Socket._instance == null) {
            Socket._instance = new Socket();
        }
        return Socket._instance;
    }
    
    _connection = null;
    connect() {
        const connectionId = "my-connection-id";

        this._connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5017/consoleHub?connectionId=" + connectionId)
            .build();

        this._connection.start()
    }
    on(event, callback) {
        this._connection.on(event, callback);
    }
    off(event, callback) {
        this._connection.off(event, callback);
    }
    // emit(event, data) {
    //     this.socket.emit(event, data);
    // }
}

export default Socket;