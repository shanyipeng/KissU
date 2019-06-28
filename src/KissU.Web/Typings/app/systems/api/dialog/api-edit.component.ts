﻿import { Component, Injector } from '@angular/core';
import { env } from '../../env';
import { EditDialogComponentBase } from '../../../util';
import { ApiViewModel } from './model/api-view-model';

/**
 * Api资源编辑页
 */
@Component({
    selector: 'api-edit',
    templateUrl: !env.dev() ? './html/edit.component.html' : '/view/systems/api/edit'
})
export class ApiEditComponent extends EditDialogComponentBase<ApiViewModel> {
    /**
     * 初始化Api资源编辑页
     * @param injector 注入器
     */
    constructor(injector: Injector) {
        super(injector);
    }

    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "api";
    }
}