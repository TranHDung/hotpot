import { Component, OnInit } from '@angular/core';
import { HotspotResultService } from '../../services/hotspotResult.service';
import { HotspotResult, FilterHotspotResult } from '../../models/HotspotResult';
import { Sorting } from '../../models/Sorting';
import { Paging } from '../../models/Paging';
import { Page } from '../../models/Page';
import { NbDialogService } from '@nebular/theme';
import { EditCalotteryComponent } from './edit-calottery/edit-calottery.component';

@Component({
  selector: 'ngx-calottery',
  templateUrl: './calottery.component.html',
  styleUrls: ['./calottery.component.scss']
})
export class CalotteryComponent implements OnInit {
  rows: HotspotResult[];
  page: Page;
  filter: FilterHotspotResult;
  loading: boolean = true;
  constructor(private _hotspotResult: HotspotResultService, private _dialogService: NbDialogService) { 
    this.page = new Page();
    this.filter = new FilterHotspotResult();
  }
  
  ngOnInit(): void {
    this.setPage({offset: 0});
  }

  setPage(pageInfo){
    this.filter.paging.top = this.page.size;
    this.page.pageNumber = pageInfo.offset || 0;
    this.filter.paging.skip = this.page.size * this.page.pageNumber;
    this.load();
  }

  onSort(sortInfo) {
    this.filter.sorting.columnName = sortInfo.column.prop;
    this.filter.sorting.sortDerection = sortInfo.newValue;
    this.load();
  }

  load(){
    this._hotspotResult.getByFilter(this.filter).subscribe(result =>{
      this.page.totalElements = result.totalCount;
      this.rows = result.data;
      this.loading = false;
    },
    error => {

    })
  }

  onAdd(){
    this._dialogService.open(EditCalotteryComponent,{context: {hotspotResultId: 0}})
    .onClose
    .subscribe(() => {

    });
  }
}
