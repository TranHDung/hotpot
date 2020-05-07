/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { PagesRoutingModule } from './pages-routing.module';
import { ThemeModule } from '../@theme/theme.module';
import { MiscellaneousModule } from './miscellaneous/miscellaneous.module';
import { PagesMenu } from './pages-menu';
import { ECommerceModule } from './e-commerce/e-commerce.module';
import { NbMenuModule, NbCalendarModule, NbCheckboxModule, NbCardModule, NbBadgeModule, NbButtonModule, NbDialogModule, NbDatepickerModule, NbInputModule } from '@nebular/theme';
import { AuthModule } from '../@auth/auth.module';
import { CalotteryComponent } from './calottery/calottery.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { EditCalotteryComponent } from './calottery/edit-calottery/edit-calottery.component';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

const PAGES_COMPONENTS = [
  PagesComponent,
];

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    DashboardModule,
    ECommerceModule,
    NbMenuModule,
    MiscellaneousModule,
    NbCalendarModule,
    NbCheckboxModule,
    NbCardModule,
    NbBadgeModule,
    NgxDatatableModule,
    NbButtonModule,
    NbInputModule,
    OwlDateTimeModule, 
    OwlNativeDateTimeModule,
    NgSelectModule, 
    FormsModule,
    AuthModule.forRoot(),
    NbDialogModule.forRoot()
  ],
  declarations: [
    ...PAGES_COMPONENTS,
    CalotteryComponent,
    EditCalotteryComponent
  ],
  providers: [
    PagesMenu,
  ],
})
export class PagesModule {
}
