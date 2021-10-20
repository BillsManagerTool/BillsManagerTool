import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchTextFilter',
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string, searchKey: string): any[] {
    // console.log(searchText);
    console.log(searchKey);
    if (items && items.length) {
      return items.filter((item) => {
        // console.log(item[searchKey]);
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
