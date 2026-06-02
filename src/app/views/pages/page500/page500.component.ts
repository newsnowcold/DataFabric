import { Component } from '@angular/core';
import { IconModule } from '@coreui/icons-angular';
import {
  ButtonDirective,
  ColComponent,
  GridModule,
  FormControlDirective,
  InputGroupComponent,
  InputGroupTextDirective,
  RowComponent
} from '@coreui/angular';

@Component({
  selector: 'app-page500',
  templateUrl: './page500.component.html',
  imports: [GridModule, RowComponent, ColComponent, InputGroupComponent, InputGroupTextDirective, IconModule, FormControlDirective, ButtonDirective]
})
export class Page500Component {}
