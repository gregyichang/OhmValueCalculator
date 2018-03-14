import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './components/app/app.component';
import { DataService } from './components/service/data.service';

@NgModule({
    bootstrap: [AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
        DataService,
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
