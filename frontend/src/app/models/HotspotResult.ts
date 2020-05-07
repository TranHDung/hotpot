import { Sorting } from './Sorting';
import { Paging } from './Paging';

export class HotspotResult {
    id: number;
    drawNumber:number;
    drawDate: Date;
    blueNumber:string[];
    yellowNumber:string;
    createAt:Date;
}


export class FilterHotspotResult {
    startSession: number;
    endSession:number;
    topSeccion:number;
    groupId:number;
    codeId:number;
    startDrawDate: Date;
    endDrawDate: Date;
    sorting: Sorting;
    paging: Paging;

    constructor() {
        this.startSession = 0;
        this.endSession = 0;
        this.topSeccion = 0;
        this.groupId = 0;
        this.codeId = 0;
        this.startDrawDate = null;
        this.endDrawDate = null;
        this.sorting = new Sorting();
        this.paging = new Paging();
    }
}