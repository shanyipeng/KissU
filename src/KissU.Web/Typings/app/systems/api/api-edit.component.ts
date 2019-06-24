﻿import { Component, Injector } from '@angular/core';
import { env } from '../../env';
import { EditComponentBase } from '../../../util';
import { ApiViewModel } from './model/api-view-model';

/**
 * Api资源编辑
 */
@Component({
    selector: 'api-edit',
    templateUrl: env.prod() ? './html/api-edit.component.html' : '/view/systems/api/edit'
})
export class ApiEditComponent extends EditComponentBase<ApiViewModel> {
    /**
     * 初始化组件
     * @param injector 注入器
     */
    constructor(injector: Injector) {
        super(injector);
    }
    
    /**
     * 创建视图模型
     */
    protected createModel() {
	    var model = new ApiViewModel();
        return model;
    }

    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "api";
    }
}