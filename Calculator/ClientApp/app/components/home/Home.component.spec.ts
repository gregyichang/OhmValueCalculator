/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { ChangeDetectorRef } from '@angular/core';
import { BaseRequestOptions, Http, HttpModule, Response, ResponseOptions, XHRBackend } from '@angular/http';
import { HomeComponent } from './home.component';
import { DataService, IColorCode, OhmValue } from '../service/data.service';
import { TestBed, async, ComponentFixture, inject } from '@angular/core/testing';
import { MockBackend } from '@angular/http/testing'
let fixture: ComponentFixture<HomeComponent>;

describe('Home component', () => {
    let dataService: DataService;
    let calcInstance: HomeComponent;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [
                MockBackend,
                { provide: 'BASE_URL', useValue: '' },
                { provide: 'XHRBackend', useClass: MockBackend },
                BaseRequestOptions,
                {
                    provider: Http,
                    useFactory: (backend: MockBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                },
                {
                    provide: DataService,
                    useFactory: (http: Http, url: string = "") => new DataService(http, url),
                    deps: [Http, String]
                },
                ChangeDetectorRef
            ]
        });

        TestBed.configureTestingModule({ declarations: [HomeComponent] });
        fixture = TestBed.createComponent(HomeComponent);
        fixture.detectChanges();
    });

    it('should display a title', async(() => {
        const titleText = fixture.nativeElement.querySelector('h1').textContent;
        expect(titleText).toEqual('Calculates the Ohm value of a resistor based on the band colors');
    }));

    it('should call service when calculate', async(inject([DataService, ChangeDetectorRef], (dataService: DataService, ref: ChangeDetectorRef) => {
        //prepare
        let spyCalculate: any;
        var homeComponent = new HomeComponent(dataService, ref);
        homeComponent.bandACode = "Red";
        homeComponent.bandBCode = "Brown";
        homeComponent.bandCCode = "Yellow";
        homeComponent.bandDCode = "Gold";
        spyCalculate = spyOn(dataService, 'calculateOhmValue').and.returnValue({minValue: 2510, maxValue: 2600});

        //action
        homeComponent.calculate();

        //assertion
        expect(dataService.calculateOhmValue).toHaveBeenCalled();
        expect(homeComponent.calcResult.minValue == 2510).toBeTruthy();
        expect(homeComponent.calcResult.minValue == 2600).toBeTruthy();
    })));
});
