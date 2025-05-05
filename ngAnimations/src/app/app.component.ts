import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [
    trigger("shake",
      [transition(':increment', useAnimation(shakeX, { params: { time: 2000 } })),

      ]),
  ]
})

export class AppComponent {
  title = 'ngAnimations';
  ng_shake = 0;

  constructor() {
  }
  animer1Fois() {
    this.ng_shake++;
  }
}

