﻿import { Component, Injector } from '@angular/core';
import { env } from '../../env';
import { EditComponentBase } from '../../../util';
import { ApiScopeViewModel } from './model/apiScope-view-model';

/**
 * Api许可范围编辑
 */
@Component({
    selector: 'apiScope-edit',
    templateUrl: env.prod() ? './html/apiScope-edit.component.html' : '/view/systems/apiScope/edit'
})
export class ApiScopeEditComponent extends EditComponentBase<ApiScopeViewModel> {
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
	    var model = new ApiScopeViewModel();
        return model;
    }

    /**
     * 获取基地址
     */
    protected getBaseUrl() {
        return "apiScope";
    }
}