import { Component, OnInit, Inject } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { TranslationService } from '../../../../../modules/i18n/translation.service';
import { DOCUMENT } from '@angular/common';

interface LanguageFlag {
  lang: string;
  name: string;
  flag: string;
  active?: boolean;
}

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss'],
})
export class LanguageSelectorComponent implements OnInit {
  placement: string = "right";
  language: LanguageFlag;
  languages: LanguageFlag[] = [
    {
      lang: 'en',
      name: 'English',
      flag: './assets/media/svg/flags/226-united-states.svg',
    },
    {
      lang: 'ar',
      name: 'Arabic',
      flag: './assets/media/svg/flags/151-united-arab-emirates.svg',
    },
    // {
    //   lang: 'ch',
    //   name: 'Mandarin',
    //   flag: './assets/media/svg/flags/015-china.svg',
    // },
    // {
    //   lang: 'es',
    //   name: 'Spanish',
    //   flag: './assets/media/svg/flags/128-spain.svg',
    // },
    // {
    //   lang: 'jp',
    //   name: 'Japanese',
    //   flag: './assets/media/svg/flags/063-japan.svg',
    // },
    // {
    //   lang: 'de',
    //   name: 'German',
    //   flag: './assets/media/svg/flags/162-germany.svg',
    // },
    // {
    //   lang: 'fr',
    //   name: 'French',
    //   flag: './assets/media/svg/flags/195-france.svg',
    // },
  ];
  constructor(
    private translationService: TranslationService,
    private router: Router,
    @Inject(DOCUMENT) private document: Document
  ) { }

  ngOnInit() {
    this.setSelectedLanguage();
    this.router.events
      .pipe(filter((event) => event instanceof NavigationStart))
      .subscribe((event) => {
        this.setSelectedLanguage();
      });
      this.changeCssFile(this.translationService.getSelectedLanguage());
  }

  setLanguageWithRefresh(lang) {
    this.setLanguage(lang);
    this.changeCssFile(lang);
    window.location.reload();
  }

  setLanguage(lang) {
    this.languages.forEach((language: LanguageFlag) => {
      if (language.lang === lang) {
        language.active = true;
        this.language = language;
      } else {
        language.active = false;
      }
    });
    this.translationService.setLanguage(lang);
  }

  setSelectedLanguage(): any {
    this.setLanguage(this.translationService.getSelectedLanguage());
  }

  changeCssFile(lang: string) {
    this.placement = lang == 'ar'? 'left': 'right';
    let htmlTag = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;
    htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    let headTag = this.document.getElementsByTagName('head')[0] as HTMLHeadElement;
    headTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    let existingLink = this.document.getElementById('langCss') as HTMLLinkElement;
    let bundleName = lang === 'ar' ? 'arabicStyle.css' : 'englishStyle.css';
    if (existingLink) {
      existingLink.href = bundleName;
    } else {
      let newLink = this.document.createElement('link');
      newLink.rel = 'stylesheet';
      newLink.type = 'text/css';
      newLink.id = 'langCss';
      newLink.href = bundleName;
      headTag.appendChild(newLink);
    }
  }
}
