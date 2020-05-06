import { Observable } from 'rxjs';
import {
    FilterHotspotResult, HotspotResult
  } from "../models/HotspotResult";
import { ResultFilter } from '../models/ResultFilter';
import { Injectable } from '@angular/core';
import { HttpService } from '../@core/backend/common/api/http.service';


@Injectable()
export class HotspotResultService {
    constructor(private _http: HttpService) {}
    getByFilter( filter: FilterHotspotResult): Observable<ResultFilter<HotspotResult>>{
        return this._http.post(`hotspotResult/filter`,filter);
    }
}
