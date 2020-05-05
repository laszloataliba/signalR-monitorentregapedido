
function SigalRConnection(pHubPath) {
    var signalRConnection = new signalR.HubConnectionBuilder()
        .withUrl(pHubPath)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    SignalRConnectionStart(signalRConnection);

    return signalRConnection;
}

function SignalRConnectionStart(pSignalRConnection) {
    pSignalRConnection
        .start()
        .then(function () {
            console.log('Conectado');

        }).catch(function (err) {
            console.log(`Erro: ${err.toString()}`);
        });
}


