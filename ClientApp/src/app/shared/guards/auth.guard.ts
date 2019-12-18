import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthHelper } from '../helpers/auth.helper';

@Injectable({ providedIn: 'root' })

export class AuthGuard implements CanActivate {
    constructor(
        private authHelper: AuthHelper,
        private router: Router
    ) { }

    async canActivate() {
        debugger;

        const isAuth: boolean = await this.authHelper.isAuth().then(res => { return res });
        // const isAuth = await this.authHelper.isAuth().then((result) => {
        //     return result;
        // });

        // const isAuth:boolean=true;

        if (isAuth) {
            return true;
        }

        this.router.navigate(['auth']);
        return false;
    }
}