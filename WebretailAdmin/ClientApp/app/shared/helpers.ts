import { Headers } from '@angular/http';
import { SelectItem } from 'primeng/primeng';

export class Helpers {

    static distinct(a: any[]) : any[] {
        let seen: any = {};
        let out: any[] = [];
        let len = a.length;
        let j = 0;
        for (let i = 0; i < len; i++) {
            let item = a[i];
            if (seen[item.label] !== 1) {
                seen[item.label] = 1;
                out[j++] = item;
            }
        }
        return out;
    }

    static getHeaders() : Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('token', localStorage.getItem('token'));
        return headers;
    }

    static newSelectItem(value: any, label?: string) : SelectItem {
        return <SelectItem>{ label: label ? label : value, value: value };
    }

    static getUnitOfMeasure() : SelectItem[] {
        return <SelectItem[]>[
            Helpers.newSelectItem('QT', 'Quatity'),
            Helpers.newSelectItem('MT', 'Meter'),
            Helpers.newSelectItem('KG', 'Kilogram')
        ];
    }
}