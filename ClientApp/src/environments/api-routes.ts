import { Injectable } from '@angular/core';

@Injectable()

export class ApiRoutes {
    private protocolHttp = 'http://localhost:62264/';
    private protocolHttps = 'https://localhost:44336/';

    public readonly mainScreenRoute = this.protocolHttp + 'api/mainscreen/';
    public readonly authentificationRoute = this.protocolHttp + 'api/authentification/';
    public readonly adminScreenRoute = this.protocolHttp + 'api/adminscreen/';

}
