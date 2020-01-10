import { LogException } from 'src/app/shared/models/log-exception';

export class ResponseGetLogsAdminScreenModel {
    totalCount: number;
    items: Array<LogException>;
}