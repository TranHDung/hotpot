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
    groupId:string;
    codeId:string;
    startDrawDate: Date;
    endDrawDate: Date;
    createAt:Date;
    sorting: Sorting;
    paging: Paging;

    constructor(sorting: Sorting,paging: Paging) {
        this.startSession = null;
        this.endSession = null;
        this.topSeccion = null;
        this.groupId = null;
        this.codeId = null;
        this.startDrawDate  = null;
        this.endDrawDate  = null;
        this.createAt = null;
        this.sorting = sorting;
        this.paging = paging;
    }
}