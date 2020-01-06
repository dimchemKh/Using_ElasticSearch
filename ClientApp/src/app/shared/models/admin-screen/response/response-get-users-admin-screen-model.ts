import { ApplicationUser } from 'src/app/shared/models/application-user';

export class ResponseGetUsersAdminScreenModel {
    totalCount: number;
    items: Array<ApplicationUser>;
}