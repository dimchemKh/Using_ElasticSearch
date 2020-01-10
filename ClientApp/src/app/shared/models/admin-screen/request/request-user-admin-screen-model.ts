import { UserRole } from 'src/app/shared/enums/user-roles';

export class RequestUserAdminScreenModel {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;
    role: UserRole;

    reset(){
        this.firstName = null;
        this.lastName = null;
        this.email = null;
        this.phoneNumber = null;
        this.password = null;
        this.role = null;
    }
}