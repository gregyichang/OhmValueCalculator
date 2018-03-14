import { Component, Input, OnInit } from '@angular/core';
import { DataService, IColorCode } from '../service/data.service';

@Component({
    providers: [DataService],
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    private colorCodes: IColorCode[];
    public colorABCodes: IColorCode[];
    public colorCCodes: IColorCode[];
    public colorDCodes: IColorCode[];
    public bandACode: string = "";
    public bandBCode: string = "";
    public bandCCode: string = "";
    public bandDCode: string = "";
    constructor(private dataService: DataService) {
    }

    ngOnInit() {
        
        this.dataService.getColorCodes().subscribe(result => {
            this.colorCodes = result.json() as IColorCode[];
            this.colorABCodes = this.colorCodes.filter(code => code.isSignificant === true);
            this.colorCCodes = this.colorCodes.filter(code => code.isMultiplier === true);
            this.colorDCodes = this.colorCodes.filter(code => code.isTolerance === true);
        }, error => console.error(error));
    }

    calculate() {
        //this.dataService.cal
    }
}
