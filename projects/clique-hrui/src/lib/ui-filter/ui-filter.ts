import { Pipe, PipeTransform } from '@angular/core';
import { UiMultiselectData } from '../ui-models';
@Pipe({
    name: 'uiFilter',
    pure: false
})
export class UiFilterPipe implements PipeTransform {
    transform(items: any[], filter: UiMultiselectData, index: any): any {
        if (!items || !filter) {
            return items;
        }
        // filter items array, items which match and return true will be
        // kept, false will be filtered out
        return items.filter(item => item.Text.indexOf(filter.Text) !== -1);
    }
}