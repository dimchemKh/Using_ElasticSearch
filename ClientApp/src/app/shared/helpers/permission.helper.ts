import { Injectable } from '@angular/core';
import { PagePermission } from '../enums/page-permission';

@Injectable({
    providedIn: 'root'
})

export class PermissionHelper {

    constructor(

    ) {

    }

    isPermissionAccess(action: string, permissionString: string, page: PagePermission): boolean {
        let str = this.setUpperCase(PagePermission[page]);

        if (permissionString.includes(str + '.Can' + action + '=true')) {
            return false;
        }
        return true;
    }

    private setUpperCase(word: string): string {
        return word.substring(0, 1).toUpperCase() + word.substring(1);
    }

}