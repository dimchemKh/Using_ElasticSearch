import { UserRole } from '../enums/user-roles';

export class ApplicationUser {
    id: string;
    firstName: string;
    lastName: string;
    role: UserRole;   
    email: string;
    phoneNumber: string;
    isRemoved: number;
}