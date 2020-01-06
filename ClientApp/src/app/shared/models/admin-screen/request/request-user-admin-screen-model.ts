import { UserRoles } from 'src/app/shared/enums/user-roles';

export class RequestUserAdminScreenModel {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    role: UserRoles;

    reset(){
        this.firstName = null;
        this.lastName = null;
        this.email = null;
        this.password = null;
        this.role = null;
    }
}