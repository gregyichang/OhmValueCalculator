import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/Observable/of';
import { Http, Response } from '@angular/http';

@Injectable()
export class DataService {
    private colorCodes: IColorCode[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {}

    getColorCodes(): Observable<Response> {
        return this.http.get(this.baseUrl + 'api/Calculator/ColorCodes');
    }

    calculateOhmValue(bandAColor: string, bandBColor: string, bandCColor: string, bandDColor: string): Observable<Response> {
        var param = { 'bankAcolor': bandAColor };
        return this.http.post(this.baseUrl + 'api/Calculator/CalculateOhmValue', param, {});
    }
}


export interface IColorCode {
    color: string;
    significantDigits: number;
    multiplier: number;
    tolerance: number;
    isSignificant: Boolean;
    isMultiplier: Boolean;
    isTolerance: Boolean
}

export interface OhmValue {
    minValue: number;
    maxValue: number;
}