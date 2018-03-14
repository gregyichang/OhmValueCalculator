import { Component, Input, OnInit } from '@angular/core';
import { DataService, IColorCode, OhmValue } from '../service/data.service';

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
    public calcResult: OhmValue;
    public calcSuccess: Boolean = false;
    constructor(private dataService: DataService) {
        //this.calcResult = null;
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
        if (this.bandACode == "" || this.bandBCode == "" || this.bandCCode == "") {
            this.reset();
            return;
        }

        this.dataService.calculateOhmValue(this.bandACode, this.bandBCode, this.bandCCode, this.bandDCode).subscribe(
            result => {
                this.calcResult = result.json() as OhmValue;
                this.calcSuccess = true
            }
            , error => {
                this.reset();
            });
    }

    reset() {
        this.calcResult = {} as OhmValue;
        this.calcSuccess = false;
    }
}
