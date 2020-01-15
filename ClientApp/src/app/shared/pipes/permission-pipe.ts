import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'toUpper'
})

export class PermissionPipe implements PipeTransform {
    transform(value: string): string {
        let permission = value.slice(3);

        return 'Can ' + permission.toLowerCase();
    }
}