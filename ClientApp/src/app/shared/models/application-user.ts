import { UserRoles } from '../enums/user-roles';

export class ApplicationUser {
    id: string;
    firstName: string;
    lastName: string;
    role: UserRoles;   
    email: string;
    phoneNumber: string;
    isRemoved: number;
}