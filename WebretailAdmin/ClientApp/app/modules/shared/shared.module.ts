import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Configuration } from './../../app.constants';
import { AuthenticationService } from './../../services/authentication.service';
import { CustomFooterComponent } from './components/customfooter/customfooter.component';
import { NavigationComponent } from './components/navigation/navigation.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        HttpModule,
    ],
    declarations: [
        NavigationComponent,
        CustomFooterComponent
    ],
    exports: [
        NavigationComponent,
        CustomFooterComponent
    ]
})

export class SharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                AuthenticationService,
                Configuration
            ]
        };
    }
}