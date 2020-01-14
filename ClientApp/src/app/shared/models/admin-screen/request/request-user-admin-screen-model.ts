import { UserRole } from 'src/app/shared/enums/user-roles';
import { PermissionModel } from '../../common/PermissionModel';

export class RequestCreateUserAdminScreenModel {
    userId: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;
    role: UserRole;

    permissions: Array<PermissionModel>;

    reset(){
        this.firstName = null;
        this.lastName = null;
        this.email = null;
        this.phoneNumber = null;
        this.password = null;
        this.role = null;
    }
}