import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchTextFilter',
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string, searchKey: string): any[] {
    if (items && items.length) {
      return items.filter((item) => {
        if (
          searchText &&
          item[searchKey].toLowerCase().indexOf(searchText.toLowerCase()) === -1
        ) {
          return false;
        }
        return true;
      });
    } else {
      return items;
    }
  }
}
