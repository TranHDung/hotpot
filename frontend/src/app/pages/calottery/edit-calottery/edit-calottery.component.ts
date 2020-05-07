import { Component, OnInit, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'ngx-edit-calottery',
  templateUrl: './edit-calottery.component.html',
  styleUrls: ['./edit-calottery.component.scss']
})
export class EditCalotteryComponent implements OnInit {
  @Input() hotspotResultId: number;
  
  title: string;
  constructor(protected ref: NbDialogRef<EditCalotteryComponent>) { }

  ngOnInit(): void {
    this.title = this.hotspotResultId > 0 ? "Update hotspot" : "Create hotspot"
  }

}
