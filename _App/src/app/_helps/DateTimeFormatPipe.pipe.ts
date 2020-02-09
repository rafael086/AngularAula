import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Contants } from '../utils/Contants';

@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipePipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Contants.DATA_TIME_FMT);
  }

}
