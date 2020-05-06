import { Component, OnInit } from '@angular/core';
import { HotspotResultService } from '../../services/hotspotResult.service';
import { HotspotResult, FilterHotspotResult } from '../../models/HotspotResult';
import { Sorting } from '../../models/Sorting';
import { Paging } from '../../models/Paging';
import { Page } from '../../models/Page';

@Component({
  selector: 'ngx-calottery',
  templateUrl: './calottery.component.html',
  styleUrls: ['./calottery.component.scss']
})
export class CalotteryComponent implements OnInit {
  rows: HotspotResult[];
  page: Page;
  constructor(private _hotspotResult: HotspotResultService) { 
    this.page = new Page();
  }
  
  ngOnInit(): void {
    this.load();
  }

  load(){
    let sorting = new Sorting();
    let paging = new Paging();
    paging.top = this.page.size;
    paging.skip = this.page.size * (this.page.pageNumber - 1);
    let filter = new FilterHotspotResult(sorting,paging);

    this._hotspotResult.getByFilter(filter).subscribe(result =>{
      this.page.totalElements = result.totalCount;
      this.rows = result.data;
    },
    error => {

    });
    
  }
}
