import { PagePermission } from '../../enums/page-permission';

export class PermissionModel {
    canView: boolean;
    canEdit: boolean;
    canCreate: boolean;
    canRemove: boolean;
    userId: string;
    page: PagePermission;
}