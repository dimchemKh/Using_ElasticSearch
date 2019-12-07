import { Injectable } from '@angular/core';

@Injectable()

export class ApiRoutes {
    private protocolHttp = 'http:/localhost:52196/';
    private protocolHttps = 'https://localhost:44336/';

    public readonly mainScreenRoute = this.protocolHttps + 'api/main/';

}
