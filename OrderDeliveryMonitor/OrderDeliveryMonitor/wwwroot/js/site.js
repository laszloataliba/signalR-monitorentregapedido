var signalRConnection = null;

async function SigalRConnection(pHubPath) {
    signalRConnection = new signalR.HubConnectionBuilder()
        .withUrl(pHubPath)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    await SignalRConnectionStart();
}

async function SignalRConnectionStart() {
    try {
        await signalRConnection
            .start()
            .then(function () {
                console.log('Conectado');

            }).catch(function (err) {
                console.log(`Erro: ${err.toString()}`);
            });

    } catch (err) {
        console.log(err);
    }
}

