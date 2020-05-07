import { Component, OnInit, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { HotspotResult } from '../../../models/HotspotResult';
import * as moment from 'moment';
import { HotspotResultService } from '../../../services/hotspotResult.service';

@Component({
  selector: 'ngx-edit-calottery',
  templateUrl: './edit-calottery.component.html',
  styleUrls: ['./edit-calottery.component.scss']
})
export class EditCalotteryComponent implements OnInit {
  @Input() hotspotResultId: number;
  
  title: string;
  hotspotResult: HotspotResult = new HotspotResult();
  numbers: any = [];
  submiting: boolean = false;
  _rerender: boolean = true;

  constructor(private _hotspotResult: HotspotResultService,protected ref: NbDialogRef<EditCalotteryComponent>) { }

  ngOnInit(): void {
    this.title = this.hotspotResultId > 0 ? "Update hotspot" : "Create hotspot"
  }

  onAdd($event){
    var number = Number($event.label);
    if(!number || number > 80){
      this.numbers = this.numbers.slice(0, -1);
      return;
    }
  }
  
  isValid(){
    return this.numbers.length == 20 && this.hotspotResult.drawDate && this.hotspotResult.drawNumber;
  }

  onCreate(){
    this.submiting = true;
    if(this.isValid()){
      this.numbers = this.numbers.map(n => n.label);
      this.hotspotResult.yellowNumber = this.numbers.slice(-1)[0];
      this.hotspotResult.blueNumbers= this.numbers.slice(0,-1);
      //this.hotspotResult.drawDate = moment(this.hotspotResult.drawDate.getTime()).format();
      this._hotspotResult.add(this.hotspotResult).subscribe(result => {
        this.ref.close();
      });
    }
  }

  onSave(){
    this.submiting = true;
  }
}
