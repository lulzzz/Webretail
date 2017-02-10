import { Component, OnInit} from '@angular/core';

import { AuthenticationService } from './../../../services/authentication.service';

@Component({
    selector: 'home-component',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

    public message: string;

    constructor(private authenticationService: AuthenticationService) {
        this.message = 'Home';
    }

	ngOnInit() {
        this.authenticationService.checkCredentials();
    }
}
