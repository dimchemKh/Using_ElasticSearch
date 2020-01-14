import { PagePermission } from '../../enums/page-permission';

export class PermissionModel {
    canView: boolean;
    canEdit: boolean;
    canCreate: boolean;
    userId: string;
    page: PagePermission;
}